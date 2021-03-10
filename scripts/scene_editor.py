# scene_editor.py

import PySimpleGUI as sg
from PIL import Image
import os
import io

SPRITE_SHEET_PATH = os.getcwd() + "/tilesheets"
TILE_SIZE = 16  # Pixels
DISPLAY_TILES_X = 16
DISPLAY_TILES_Y = 10
TILES_X = 32
TILES_Y = 20


class Tile:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.texture_rectangle = (0, 0, TILE_SIZE, TILE_SIZE)

class SceneEditorGUI:
    def __init__(self):
        # Setting up tile grid
        self.tile_grid = [[Tile(i, j) for i in range(TILES_X)] for j in range(TILES_Y)]
        # Setting up user interface
        layout = self.create_layout()
        self.window = sg.Window(title="Scene Editor", layout=layout)

    def create_layout(self):
        # First column
        file_list_column = [
            [sg.Button("Refresh", key="-REFRESH-")],
            [sg.Text("Select the sprite sheet:")],
            [sg.Listbox(values=[], enable_events=True, size=(65, 5), key="-FILE LIST-")],
            [sg.Button("Exit")],
        ]
        # Second column
        image_viewer_column = [[sg.Image(key="-IMAGE_" + str(i) + "_" + str(j) + "_", pad=(0,0)) for i in range(DISPLAY_TILES_X)] for j in range(DISPLAY_TILES_Y)]
        # Bringing together columns
        layout = [
            [
                sg.Column(file_list_column),
                sg.VSeperator(),
                sg.Column(image_viewer_column)
            ]
        ]
        return layout

    def convert_to_bytes(self, tile, file_path):
        img = Image.open(file_path)
        # cur_width, cur_height = img.size
        img = img.crop(box=(tile.texture_rectangle[0], tile.texture_rectangle[1], tile.texture_rectangle[2], tile.texture_rectangle[3]))    
        bio = io.BytesIO()
        img.save(bio, format="PNG")
        del img
        return bio.getvalue()

    def loop(self):
        while True:
            event, values = self.window.read()
            if event == "Exit" or event == sg.WIN_CLOSED:
                break
            if event == "-REFRESH-":
                self.update_filelist(SPRITE_SHEET_PATH)
            elif event == "-FILE LIST-":  # A file was chosen from the listbox
                filename = os.path.join(SPRITE_SHEET_PATH, values["-FILE LIST-"][0])
                for j in range(DISPLAY_TILES_Y):
                    for i in range(DISPLAY_TILES_X):
                        self.window["-IMAGE_" + str(i) + "_" + str(j) + "_"].update(data=self.convert_to_bytes(self.tile_grid[j][i], filename))
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
