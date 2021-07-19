import pcwares

mf = pcwares.ReadManufacturers("manufacturers.csv")

for m in mf:
	print(m.description())