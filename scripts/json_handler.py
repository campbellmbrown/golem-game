import json
from tile import MapTile
# from types import Namespace

class JsonSceneManager:

    @staticmethod
    def serialize(map_tiles):
        json_scene = JsonScene()

        for tiles_y in map_tiles:  # HEIGHT
            for tile in tiles_y:  # WIDTH
                json_scene.tiles.append(JsonTile([tile.x, tile.y], tile.texture_rectangle))
        
        # WIDTH, HEIGHT
        json_scene.set_size((len(map_tiles[0]), len(map_tiles)))
        
        json_str = json_scene.toJSON()
        destination = open("scene.json", "w+")
        destination.write(json_str)
        destination.close()

    @staticmethod
    def deserialize(json_path):

        f = open(json_path, "r")
        json_str = f.read()
        deserialized_data = json.loads(json_str)
        
        tiles = deserialized_data["tiles"]
        size = deserialized_data["size"]
        # Initialising list
        map_tiles = [[0 for _ in range(size[0])] for _ in range(size[1])]
        for tile in tiles:
            jsonTile = JsonTile(tile["position"], tile["texture_rectangle"])
            map_tiles[jsonTile.position[1]][jsonTile.position[0]] = jsonTile.jsonTileToMapTile()

        return map_tiles

class JsonScene:
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
            sort_keys=True, indent=None)

    def __init__(self):
        self.size = (0, 0)  # WIDTH, HEIGHT
        self.tiles = []

    def set_size(self, size):
        self.size = size


class JsonTile:
    def __init__(self, position, texture_rectangle):
        self.position = position
        self.texture_rectangle = texture_rectangle

    def jsonTileToMapTile(self):
        map_tile = MapTile(self.position[0], self.position[1])
        map_tile.texture_rectangle = self.texture_rectangle
        return map_tile
    