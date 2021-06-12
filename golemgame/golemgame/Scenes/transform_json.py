file_name = "scene.json"

file = open("scene.json", "r")
old_string = file.read()
file.close()

without_new_lines = old_string.replace("\n", "")
new_string = without_new_lines.replace(" ", "")

print(new_string)

file = open("scene.json", "w")
file.write(new_string)
file.close()
