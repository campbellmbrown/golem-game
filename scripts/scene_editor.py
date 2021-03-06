# scene_editor.py

import PySimpleGUI as sg
import PIL
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
        
        image_viewer_column = [
            [sg.Image(key="-IMAGE-")],
        ]

        layout = [
            [
                sg.Column(file_list_column),
                sg.VSeperator(),
                sg.Column(image_viewer_column)
            ]
        ]
        return layout

    def convert_to_bytes(self, file_path, resize=None):
        print(file_path)
        img = PIL.Image.open(file_path)
        cur_width, cur_height = img.size
        print("Width: {}".format(cur_width))
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
                    # Get list of files in folder
                    file_list = os.listdir(folder)
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
                try:
                    filename = os.path.join(
                        values["-FOLDER-"], values["-FILE LIST-"][0]
                    )
                    print(filename)
                    print(type(filename))
                    self.window["-IMAGE-"].update(data=self.convert_to_bytes(filename))
                except:
                    pass
        self.window.close()
    
def run():
    scene_editor_gui = SceneEditorGUI()
    scene_editor_gui.loop()

if __name__ == "__main__":
    run()
