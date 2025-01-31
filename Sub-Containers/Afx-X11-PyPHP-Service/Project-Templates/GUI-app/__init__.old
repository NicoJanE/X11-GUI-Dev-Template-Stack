# BEGIN  Initialization file for the main project directory
#
# Purpose 1. Package Namespace Definition Methods (Controlling Imports)
# --------------------------------------------------------------
#
# 1.1   from .call_rust import *                  
#       Be careful—this imports all public functions and classes from 'call_rust' in the current directory,
#       but bypasses the package namespace. All imported functions are public and can be called directly 
#       (e.g., MyFunc1()), but NOT with the namespace (e.g., src_gui.MyFunc1) due to the * import.
#
#       IMPORTANT: Importing with * pollutes the local namespace and obscures where functions originate.
#       Avoid using * imports; they can lead to ambiguity and are not recommended. 
#       
#       For accessing items through the namespace, use `import src_gui` instead, which enables `src_gui.MyFunc1`.
#
# 1.2   __all__ = ['MyFunc1', 'MyFunc2']          
#       Controls what is imported when using `from src_gui import *`. This includes ONLY MyFunc1 and MyFunc2
#       in the import, keeping other functions (e.g., MyFunc3...MyFunc99) private.
#
# 1.3   from .call_rust import MyFunc1, MyFunc2, MyFunc3, MyFunc4                                        
#       Preferred approach for public access:
#       Use `import src_gui` to import the module, making MyFunc1 through MyFunc4 accessible through
#       the `src_gui` namespace without polluting the local scope. 
#           
#           Use: from . Import directoryname to import the items
#
#
# Note 1.   Although this example uses functions, the same applies to classes.
# Note 2.   By convention, private functions or classes start with an underscore (_).
# Note 3.   Even if a class or function is "private" (e.g., _MyClass3) and not listed in `__init__.py`, 
#           it can still be imported directly with its full qualified name 
#           (e.g., `from src_gui.call_rust import _MyClass3`). This is discouraged as it breaks encapsulation.
# Note 4.   You can use aliases to enhance readability, avoid conflicts, or abbreviate:
#               `import src_gui as sg`           
# Note 5    Global variables can be exported from a module, but changes to them WON't propagate automatically 
#           to calling modules. When imported, they keep their initial values, similar to a 'const' in C.". To return
#           The update value you must define a get function in the module that explicitly returns it.
#           Defining a variable in __init__.py makes it accessible at the package level (e.g., src_gui.nje). but still
#           behavior remians const(use a function to retrieve the updated value, only usefull when intention is to be constant)
#
#
# Purpose 2. Package Initialization
# --------------------------------------------------------------
# Code here runs automatically when the package is imported. 
#
#
# Purpose 3. Conditional Imports
# --------------------------------------------------------------
# Example:
#           if sys.platform == "win32":
#               from .windows_specific import WinFeature  
#           elif sys.platform == "linux":
#               from .linux_specific import LinuxFeature 
#
#
# Purpose 4. Version and Metadata
# --------------------------------------------------------------
# This can hold version information and metadata:
#
#       __version__ = '1.0.0'
#       __author__ = 'Your Name'
#       __license__ = "MIT"          
#
# This list is not exhaustive but covers the most essential features.
#
# END  Initialization file for the main project directory


