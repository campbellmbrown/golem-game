from PIL import Image

TILE_SIZE = 16  # Pixels

class MapTile:
    def __init__(self, x, y, texture_rectangle=(0, 0, TILE_SIZE, TILE_SIZE)):
        self.x = x
        self.y = y
        self.texture_rectangle = texture_rectangle
        self.data = 0

    def __repr__ (self):
        return "MapTile(x=" + str(self.x) + ", y=" + str(self.y) + ", texture_rectangle=" + str(self.texture_rectangle) + ")"

    def reset_texture_rectangle(self):
        self.texture_rectangle = (0, 0, TILE_SIZE, TILE_SIZE)

class AvaliableTile:
    def __init__(self, x, y):
        self.x = x
        self.y = y
        self.texture_rectangle = (x * TILE_SIZE, y * TILE_SIZE, (x + 1) * TILE_SIZE, (y + 1) * TILE_SIZE)
        self.data = 0

class Spritesheet:
    def __init__(self, path):
        self.path = path
        self.width, self.height = self.find_size(path)

    def find_size(self, path):
        img = Image.open(path)
        width, height = img.size
        return [int(width / TILE_SIZE), int(height / TILE_SIZE)]