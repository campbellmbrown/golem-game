# scene_editor.py

from PIL import Image
from scene_json_serializer import JsonSceneManager
from tile import MapTile, Spritesheet, AvaliableTile
from colorlog import ColoredFormatter

import PySimpleGUI as sg
import keyboard as kb
import os
import io
import logging

SPRITE_SHEET_PATH = os.getcwd() + "/tilesheets/tilesheet.png"
BLANK_TILE_PATH = os.getcwd() + "/scene_editor_textures/blank_tile.png"
DISPLAY_TILES_X = 16
DISPLAY_TILES_Y = 10
TILES_X = 18
TILES_Y = 12
MAX_OFFSET_X = TILES_X - DISPLAY_TILES_X - 1
MAX_OFFSET_Y = TILES_Y - DISPLAY_TILES_Y - 1
SCALE = 2

# Logging
logger = logging.getLogger('gui_logger')
stream = logging.StreamHandler()
LogFormat = " %(reset)s%(log_color)s%(levelname)-8s%(reset)s | %(log_color)s%(message)s"
stream.setFormatter(ColoredFormatter(LogFormat, log_colors={
    'DEBUG':    'white',
    'INFO':     'cyan',
    'WARNING':  'yellow',
    'ERROR':    'red',
    'CRITICAL': 'black,bg_red',
}))
logger.addHandler(stream)
logger.setLevel(logging.DEBUG)

class SceneEditorGUI:
    def __init__(self):
        ''' Initialises the scene editor GUI.
        '''

        logger.info("Decreasing element padding.")
        sg.SetOptions(element_padding=(0,0))    

        logger.info("Setting up tile grid.")
        self.tile_grid = [[MapTile(i, j) for i in range(TILES_X)] for j in range(TILES_Y)]
        self.offset_x = 0
        self.offset_y = 0

        logger.info("Selecting spritesheet.")
        self.spritesheet = Spritesheet(SPRITE_SHEET_PATH)
        logger.info("Creating tile list from spritesheet.")
        self.avaliable_tile_grid = [[AvaliableTile(i, j) for i in range(self.spritesheet.width)] for j in range(self.spritesheet.height)]

        logger.info("Creating layout of GUI.")
        layout = self.create_layout()
        logger.info("Creating window.")
        self.window = sg.Window(title="Scene Editor", layout=layout, finalize=True)

        self.clear_map_grid()

        logger.info("Filling in the spritesheet.")
        for j in range(self.spritesheet.height):
            for i in range(self.spritesheet.width):
                key = self.get_key_from_grid("AVALIABLE", i, j)
                tile = self.avaliable_tile_grid[j][i]
                # Reading the image and extracting bytes data
                tile.data = self.convert_image_to_bytes(tile, self.spritesheet.path)
                # Updating the image with the tile image
                self.window[key].update(data=tile.data)

        logger.info("Setting up the selected tile.")
        self.current_tile_data = self.avaliable_tile_grid[0][0].data
        self.current_tile_texture_rectangle = self.avaliable_tile_grid[0][0].texture_rectangle
        self.window["-CURRENT_TILE-"].update(data=self.current_tile_data)

        logger.info("Initialization done!")

    def get_key_from_grid(self, key, i_idx, j_idx):
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
        img = img.resize((int(cur_width * SCALE), int(cur_height * SCALE)), Image.NEAREST)
        bio = io.BytesIO()
        img.save(bio, format="PNG")
        del img
        return bio.getvalue()

    def loop(self):
        while(True):
            # Checking events
            event, _ = self.window.read()
            if event == "Exit" or event == sg.WIN_CLOSED:
                logger.info("Exit pressed!")
                break
            elif (event == "Down"):
                self._down_action()
            elif (event == "Up"):
                self._up_action()
            elif (event == "Right"):
                self._right_action()
            elif (event == "Left"):
                self._left_action()
            elif (event == "Save"):
                self._save_action()
            elif (event == "Load"):
                self._load_action()
            for j in range(self.spritesheet.height):
                for i in range(self.spritesheet.width):
                    if event == (self.get_key_from_grid("AVALIABLE", i, j)):
                        logger.info("New tile chosen (grid index: %d, %d).", j, i)
                        self.current_tile_data = self.avaliable_tile_grid[j][i].data
                        self.current_tile_texture_rectangle = self.avaliable_tile_grid[j][i].texture_rectangle
                        self.window["-CURRENT_TILE-"].update(data=self.current_tile_data)
            for j in range(DISPLAY_TILES_Y):
                for i in range(DISPLAY_TILES_X):
                    key = self.get_key_from_grid("MAP", i, j)
                    if event == (key):
                        logger.info("Tile changed (grid index: %d, %d).", j, i)
                        self.tile_grid[j + self.offset_y][i + self.offset_x].data = self.current_tile_data
                        self.tile_grid[j + self.offset_y][i + self.offset_x].texture_rectangle = self.current_tile_texture_rectangle
                        self.window[key].update(data=self.current_tile_data)

        self.window.close()

    def _up_action(self):
        if (self.offset_y > 0):
            logger.info("Up pressed!")
            self.offset_y -= 1
            self.update_display_grid()
        else:
            logger.warning("Reached top edge - cannot go any futher.")

    def _down_action(self):
        if (self.offset_y <= MAX_OFFSET_Y):
            logger.info("Down pressed!")
            self.offset_y += 1
            self.update_display_grid()
        else:
            logger.warning("Reached bottom edge - cannot go any futher.")

    def _right_action(self):
        if (self.offset_x <= MAX_OFFSET_X):
            logger.info("Right pressed!")
            self.offset_x += 1
            self.update_display_grid()
        else:
            logger.warning("Reached right edge - cannot go any futher.")

    def _left_action(self):
        if (self.offset_x > 0):
            logger.info("Left pressed!")
            self.offset_x -= 1
            self.update_display_grid()
        else:
            logger.warning("Reached left edge - cannot go any futher.")

    def _save_action(self):
        logger.info("Save pressed!")
        self.serialize_scene()

    def _load_action(self):
        logger.info("Load pressed!")
        logger.info("File select window opened.")
        layout = [[sg.Text("Choose a file: "), sg.Input(key="-FILE PATH-"), sg.FileBrowse()], [sg.Button("Done"), sg.Button("Cancel")]]
        window = sg.Window("File Browser", layout, modal=True)
        choice = None
        while True:
            event, values = window.read()
            if event == "Done":
                self.deserialize_scene(values["-FILE PATH-"])
                break
            elif event == "Cancel" or event == sg.WIN_CLOSED:
                logger.info("File select window closed.")
                break
        window.close()

    def clear_map_grid(self):
        logger.info("Defaulting map grid to blank tiles.")
        for j in range(TILES_Y):
            for i in range(TILES_X):
                self.tile_grid[j][i].reset_texture_rectangle()
                self.tile_grid[j][i].data = self.convert_image_to_bytes(self.tile_grid[j][i], BLANK_TILE_PATH)
        self.update_display_grid()
            
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
        fnames = [f for f in file_list
            if os.path.isfile(os.path.join(folder, f))
            and f.lower().endswith((".png"))
        ]
        self.window["-FILE LIST-"].update(fnames)
    
    def serialize_scene(self):
        json_scene = JsonSceneManager(self.tile_grid)
        json_scene.serialize()

    def deserialize_scene(self, file_path):

        logger.info("Loading data from file at: %s", file_path)
        f = open(file_path, "r")
        data = f.read()


        # todo move
        self.clear_map_grid()
    

def run():
    scene_editor_gui = SceneEditorGUI()
    scene_editor_gui.loop()

if __name__ == "__main__":
    run()
