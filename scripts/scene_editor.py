# scene_editor.py

from PIL import Image
from scene_json_serializer import JsonSceneManager
from tile import MapTile, Spritesheet, AvaliableTile

import PySimpleGUI as sg
import keyboard as kb
import os
import io

SPRITE_SHEET_PATH = os.getcwd() + "/tilesheets/stardew_tilesheet.png"
BLANK_TILE_PATH = os.getcwd() + "/scene_editor_textures/blank_tile.png"
DISPLAY_TILES_X = 16
DISPLAY_TILES_Y = 10
TILES_X = 32
TILES_Y = 20
MAX_OFFSET_X = TILES_X - DISPLAY_TILES_X - 1
MAX_OFFSET_Y = TILES_Y - DISPLAY_TILES_Y - 1
SCALE = 2

class SceneEditorGUI:
    def __init__(self):
        sg.SetOptions(element_padding=(0,0))    

        # Setting up tile grid
        self.tile_grid = [[MapTile(i, j) for i in range(TILES_X)] for j in range(TILES_Y)]
        self.offset_x = 0
        self.offset_y = 0

        self.spritesheet = Spritesheet(SPRITE_SHEET_PATH)
        self.avaliable_tile_grid = [[AvaliableTile(i, j) for i in range(self.spritesheet.width)] for j in range(self.spritesheet.height)]

        # Setting up user interface
        layout = self.create_layout()
        self.window = sg.Window(title="Scene Editor", layout=layout, finalize=True)
        
        # Defaulting map grid to blank tiles
        for j in range(TILES_Y):
            for i in range(TILES_X):
                self.tile_grid[j][i].data = self.convert_image_to_bytes(self.tile_grid[j][i], BLANK_TILE_PATH)
        self.update_display_grid()

        # Filling in the avaliable tile grid
        for j in range(self.spritesheet.height):
            for i in range(self.spritesheet.width):
                key = self.get_key_from_grid("AVALIABLE", i, j)
                tile = self.avaliable_tile_grid[j][i]
                # Reading the image and extracting bytes data
                tile.data = self.convert_image_to_bytes(tile, self.spritesheet.path)
                # Updating the image with the tile image
                self.window[key].update(data=tile.data)

        self.current_tile_data = self.avaliable_tile_grid[0][0].data
        self.current_tile_texture_rectangle = self.avaliable_tile_grid[0][0].texture_rectangle
        self.window["-CURRENT_TILE-"].update(data=self.current_tile_data)
        

    def get_key_from_grid(self, key, i_idx, j_idx):
        '''
        Generates a key for a PySimpleGUI element from a grid.
            
            Parameters:
                key   (str): The main key of the element
                i_idx (int): The i index of the element (x-axis)
                j_idx (int): The j index of the element (y-axis)
            Returns:
                grid_key (str): PySimpleGUI grid element key
        '''
        grid_key = "-" + key + "_" + str(i_idx) + "_" + str(j_idx) + "-"
        return grid_key

    def create_layout(self):
        # Creating the avaliable tiles section
        avaliable_column = [[sg.Text("Avaliable tiles:")]]
        for j in range(self.spritesheet.height):
            avaliable_column.append([sg.Image(key=self.get_key_from_grid("AVALIABLE", i, j), enable_events=True) for i in range(self.spritesheet.width)])

        # Creating the map section
        map_column = [[sg.Text("Map:")]]
        for j in range(DISPLAY_TILES_Y):
            map_column.append([sg.Image(key=self.get_key_from_grid("MAP", i, j), enable_events=True) for i in range(DISPLAY_TILES_X)])

        # Creating the information section
        information_column = [
            [sg.Text("Selected tile:")],
            [sg.Image(key="-CURRENT_TILE-")],
            [sg.Button("Up"), sg.Button("Down"), sg.Button("Left"), sg.Button("Right")],
            [sg.Button("Save"), sg.Button("Load")]
        ]        
        
        # Bringing together section
        layout = [
            [sg.Column(information_column)],
            [sg.Column(avaliable_column), sg.VSeperator(), sg.Column(map_column)],
            [sg.Button("Exit")]
        ]

        return layout

    def convert_image_to_bytes(self, tile, file_path):
        img = Image.open(file_path)
        img = img.crop(box=(tile.texture_rectangle[0], tile.texture_rectangle[1], tile.texture_rectangle[2], tile.texture_rectangle[3]))
        cur_width, cur_height = img.size
        img = img.resize((int(cur_width * SCALE), int(cur_height * SCALE)), Image.ANTIALIAS)
        bio = io.BytesIO()
        img.save(bio, format="PNG")
        del img
        return bio.getvalue()

    def loop(self):
        while(True):
            # Checking events
            event, _ = self.window.read()
            # Exit condition
            if event == "Exit" or event == sg.WIN_CLOSED:
                break
            elif (event == "Down") and (self.offset_y <= MAX_OFFSET_Y):
                self.offset_y += 1
                self.update_display_grid()
            elif (event == "Up") and (self.offset_y > 0):
                self.offset_y -= 1
                self.update_display_grid()
            elif (event == "Right") and (self.offset_x <= MAX_OFFSET_X):
                self.offset_x += 1
                self.update_display_grid()
            elif (event == "Left") and (self.offset_x > 0):
                self.offset_x -= 1
                self.update_display_grid()
            elif (event == "Save"):
                self.serialize_scene()
            for j in range(self.spritesheet.height):
                for i in range(self.spritesheet.width):
                    if event == (self.get_key_from_grid("AVALIABLE", i, j)):
                        self.current_tile_data = self.avaliable_tile_grid[j][i].data
                        self.current_tile_texture_rectangle = self.avaliable_tile_grid[j][i].texture_rectangle
                        self.window["-CURRENT_TILE-"].update(data=self.current_tile_data)
            for j in range(DISPLAY_TILES_Y):
                for i in range(DISPLAY_TILES_X):
                    key = self.get_key_from_grid("MAP", i, j)
                    if event == (key):
                        self.tile_grid[j + self.offset_y][i + self.offset_x].data = self.current_tile_data
                        self.tile_grid[j + self.offset_y][i + self.offset_x].texture_rectangle = self.current_tile_texture_rectangle
                        self.window[key].update(data=self.current_tile_data)

        self.window.close()
            
    def update_display_grid(self):
        for j in range(DISPLAY_TILES_Y):
            for i in range(DISPLAY_TILES_X):
                key = self.get_key_from_grid("MAP", i, j)
                tile_data = self.tile_grid[j + self.offset_y][i + self.offset_x].data
                self.window[key].update(data=tile_data)

    def update_filelist(self, folder):
        try:
            file_list = os.listdir(folder)  # Get list of files in folder
        except:
            file_list = []
        fnames = [
            f
            for f in file_list
            if os.path.isfile(os.path.join(folder, f))
            and f.lower().endswith((".png"))
        ]
        self.window["-FILE LIST-"].update(fnames)
    
    def serialize_scene(self):
        json_scene = JsonSceneManager(self.tile_grid)
        json_scene.serialize()

def run():
    scene_editor_gui = SceneEditorGUI()
    scene_editor_gui.loop()

if __name__ == "__main__":
    run()
