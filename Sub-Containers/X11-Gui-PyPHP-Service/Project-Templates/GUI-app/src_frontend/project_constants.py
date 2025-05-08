# Project constants, recommend method for import:
#       'import project_constants as pc'   
#       Then use: pc.LINUX
#

from enum import Enum

class OS(Enum):
    LINUX = "linux"
    WIN = "win"