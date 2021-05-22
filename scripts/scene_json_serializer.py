import json
from tile import MapTile

class JsonSceneManager:
    def __init__(self, map_tiles):
        self.map_tiles = map_tiles
        self.json_scene = JsonScene()

    def serialize(self):
        for tiles_y in self.map_tiles:
            for tile in tiles_y:
                self.json_scene.tiles.append(JsonTile([tile.x, tile.y], tile.texture_rectangle))
        
        json_str = self.json_scene.toJSON()
        destination = open("scene.json", "w+")
        destination.write(json_str)
        destination.close()

    @staticmethod
    def deserialize(self):
        # todo
        return -1

class JsonScene:
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
            sort_keys=True, indent=4)

    def __init__(self):
        self.tiles = []


class JsonTile:
    def __init__(self, position, texture_rectangle):
        self.position = position
        self.texture_rectangle = texture_rectangle
    