# scene_editor.py

import PySimpleGUI as sg
from PIL import Image
import os.path
import io

class SceneEditorGUI:
    def __init__(self):
        layout = self.create_layout()
        self.window = sg.Window(title="Scene Editor", layout=layout)

    def create_layout(self):
        file_list_column = [
            [sg.Text("Choose the path of the sprite sheet:")],
            [
                sg.Text("Path:"),
                sg.In(size=(50, 1), enable_events=True, key="-FOLDER-"),
                sg.FolderBrowse(),
            ],
            [sg.Text("Select the sprite sheet:")],
            [sg.Listbox(values=[], enable_events=True, size=(65, 5), key="-FILE LIST-")],
            [sg.Button("Exit")],
        ]
        
        image_viewer_column = [[sg.Image(key="-IMAGE_" + str(i) + "_" + str(j) + "_", pad=(0,0)) for j in range(32)] for i in range(32)]

        layout = [
            [
                sg.Column(file_list_column),
                sg.VSeperator(),
                sg.Column(image_viewer_column)
            ]
        ]
        return layout

    def convert_to_bytes(self, file_path, resize=None):
        img = Image.open(file_path)
        print(img.size)
        cur_width, cur_height = img.size
        if resize:
            new_width, new_height = resize
            scale = min(new_height/cur_height, new_width/cur_width)
            img = img.resize((int(cur_width*scale), int(cur_height*scale)), PIL.Image.ANTIALIAS)
        bio = io.BytesIO()
        img.save(bio, format="PNG")
        del img
        return bio.getvalue()

    def loop(self):
        while True:
            event, values = self.window.read()
            if event == "Exit" or event == sg.WIN_CLOSED:
                break
            if event == "-FOLDER-":
                folder = values["-FOLDER-"]
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
            elif event == "-FILE LIST-":  # A file was chosen from the listbox
                # try:
                filename = os.path.join(values["-FOLDER-"], values["-FILE LIST-"][0])
                for i in range(32):
                    for j in range(32):
                        self.window["-IMAGE_" + str(i) + "_" + str(j) + "_"].update(data=self.convert_to_bytes(filename))
                # except:
                #     print("error")
        self.window.close()
    
def run():
    scene_editor_gui = SceneEditorGUI()
    scene_editor_gui.loop()

if __name__ == "__main__":
    run()
