# scene_editor.py

import PySimpleGUI as sg
from PIL import Image
import os
import io

SPRITE_SHEET_PATH = os.getcwd() + "/tilesheets"
RESOURCES_PATH = os.getcwd() + "/scene_editor_textures"
TILE_SIZE = 16  # Pixels
DISPLAY_TILES_X = 16
DISPLAY_TILES_Y = 10
TILES_X = 32
TILES_Y = 20


class MapTile:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.texture_rectangle = (0, 0, TILE_SIZE, TILE_SIZE)
        self.data = 0

class AvaliableTile:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.texture_rectangle = (x * TILE_SIZE, y * TILE_SIZE, (x + 1) * TILE_SIZE, TILE_SIZE)
        self.data = 0

class Spritesheet:
    def __init__(self, path):
        self.path = path
        self.width, self.height = self.find_size(path)

    def find_size(self, path):
        img = Image.open(path)
        width, height = img.size
        return [int(width / TILE_SIZE), int(height / TILE_SIZE)]

class SceneEditorGUI:
    def __init__(self):
        # Setting up tile grid
        self.tile_grid = [[MapTile(i, j) for i in range(TILES_X)] for j in range(TILES_Y)]

        blank_path = os.path.join(RESOURCES_PATH, "blank_tile.png")
        spritesheet_path = os.path.join(SPRITE_SHEET_PATH, "test_tilesheet.png")
        self.spritesheet = Spritesheet(spritesheet_path)
        self.avaliable_tile_grid = [[AvaliableTile(i, j) for i in range(self.spritesheet.width)] for j in range(self.spritesheet.height)]

        # Setting up user interface
        layout = self.create_layout()
        self.window = sg.Window(title="Scene Editor", layout=layout, finalize=True)
        
        # Defaulting map grid to blank tile
        for j in range(DISPLAY_TILES_Y):
            for i in range(DISPLAY_TILES_X):
                key = self.get_key_from_grid("MAP", i, j)
                self.window[key].update(data=self.convert_image_to_bytes(self.tile_grid[j][i], blank_path))

        # Filling in the avaliable tile grid
        for j in range(self.spritesheet.height):
            for i in range(self.spritesheet.width):
                key = self.get_key_from_grid("AVALIABLE", i, j)
                tile = self.avaliable_tile_grid[j][i]
                # Reading the image and extracting bytes data
                self.convert_image_to_bytes(tile, self.spritesheet.path)
                tile_data = tile.data
                # Updating the image with the tile image
                self.window[key].update(data=tile_data)

        self.window["-CURRENT_TILE-"].update(data=self.avaliable_tile_grid[0][0].data)

    def get_image_element(self, key, i_idx, j_idx):
        return self.window[(self.get_key_from_grid(key, i_idx, j_idx))]

    def get_key_from_grid(self, key, i_idx, j_idx):
        return "-" + key + "_" + str(i_idx) + "_" + str(j_idx) + "_"

    def create_layout(self):
        # Creating the avaliable tiles section
        avaliable_column = [
             [sg.Text("Avaliable tiles:")],
        ]
        for j in range(self.spritesheet.height):
            avaliable_column.append([sg.Image(key=self.get_key_from_grid("AVALIABLE", i, j), pad=(0,0), enable_events=True) for i in range(self.spritesheet.width)])

        # Creating the map section
        map_column = [
            [sg.Text("Map:")],
        ]
        for j in range(DISPLAY_TILES_Y):
            map_column.append([sg.Image(key=self.get_key_from_grid("MAP", i, j), pad=(0,0), enable_events=True) for i in range(DISPLAY_TILES_X)])

        # Creating the information section
        information_column = [
            [sg.Text("Selected tile:")],
            [sg.Image(key="-CURRENT_TILE-")],
            [sg.Text("Avaliable tiles:")],
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
        bio = io.BytesIO()
        img.save(bio, format="PNG")
        del img
        tile.data = bio.getvalue()
        return bio.getvalue()

    def loop(self):
        while True:
            event, values = self.window.read()
            if event == "Exit" or event == sg.WIN_CLOSED:
                break
            '''
            if event == "-REFRESH-":
                self.update_filelist(SPRITE_SHEET_PATH)
            elif event == "-FILE LIST-":  # A file was chosen from the listbox
                # filename = os.path.join(SPRITE_SHEET_PATH, values["-FILE LIST-"][0])
                # for j in range(DISPLAY_TILES_Y):
                #     for i in range(DISPLAY_TILES_X):
                #         self.window["-IMAGE_" + str(i) + "_" + str(j) + "_"].update(data=self.convert_image_to_bytes(self.tile_grid[j][i], filename))
            for j in range(DISPLAY_TILES_Y):
                for i in range(DISPLAY_TILES_X):
                    if event == ("-IMAGE_" + str(i) + "_" + str(j) + "_"):
                        print("{}, {}".format(i, j))
            '''
            continue
        self.window.close()

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
    
def run():
    scene_editor_gui = SceneEditorGUI()
    scene_editor_gui.loop()

if __name__ == "__main__":
    run()
