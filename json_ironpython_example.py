import clr
clr.AddReferenceToFileAndPath('JSONIronPythonExample.dll')
import JSONIronPythonExample as js # this is the name of the Namespace, NOT the DLL filename.

class SomeClass:
	def __init__(self):
		self.data = js.ParentDataClass()

	def some_method(self):
		return self.data.AnInteger * len(self.data.AList)

	def some_other_method(self):
		return self.data.ACustomType.AString

	def store_in_file(self, filename):
		with open(filename, 'wb') as f:
			f.write(self.data.ToJSONString())

	@staticmethod
	def read_from_file(filename):
		with open(filename, 'rb') as f:
			filestr = f.read()

		new_c = SomeClass()
		new_c.data = js.ParentDataClass.FromJSONString(filestr)
		return new_c


sc1 = SomeClass()
sc1.data.AnInteger = 3;
sc1.data.AList.Add('hello') # note that in this case, it's NOT a python list, it's a .NET IList
sc1.data.ACustomType.AString = 'some string'
sc1.store_in_file('class_instance.json')

sc2 = SomeClass.read_from_file('class_instance.json')
for attr in dir(sc2.data):
	print '%s: %s' %(attr, getattr(sc2.data, attr))

print sc2.some_method()
print sc2.some_other_method()