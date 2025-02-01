import sys
from PySide6.QtCore import QPropertyAnimation, QRect
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QWidget

# class to animate the drawer behavior
class DrawerAnimation():
    def __init__(self,widgetElement):
        
        # Create a new animation widget element with QMainWindow as parent
        # the new widget is triggered by the main button
        self.drawer_obj = QWidget(widgetElement)
        self.drawer_obj.setStyleSheet("background-color: lightblue;")
        self.drawer_obj.setGeometry(0, 80, 0, 200)  # Start with zero width
        self.is_open = False
        
        # Connect the animation widget to Animation object
        self.drawer_animation = QPropertyAnimation(self.drawer_obj, b"geometry")
        self.drawer_animation.setDuration(500)  # 500ms duration


    def animate_open(self):
        self.drawer_animation.setStartValue(QRect(0, 80, 0, 200))
        self.drawer_animation.setEndValue(QRect(0, 80, 200, 200))
        self.drawer_animation.start()
        self.is_open = True


    def animate_close(self):
        self.drawer_animation.setStartValue(QRect(0, 80, 200, 200))
        self.drawer_animation.setEndValue(QRect(0, 80, 0, 200))
        self.drawer_animation.start()
        self.is_open = False
        
    @property    
    def name(self):
        return self.is_open


# Main application window class
class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Animated Drawer Example")
        self.setGeometry(100, 100, 400, 300)
        
        # Create a main button to trigger the drawer animation
        self.drawer_button = QPushButton("Open Drawer", self)
        self.drawer_button.setGeometry(150, 20, 100, 40)
        self.drawer_button.clicked.connect(self.toggle_drawer) # signal click handler
        self.da = DrawerAnimation(self) # Create the animated widget used by signal handler


    # The signal handler forthe drawer button
    def toggle_drawer(self):            
        """Animate the drawer to open or close."""
        if not self.da.is_open:
            self.da.animate_open()
            self.drawer_button.setText("Close Drawer")
        else:
            self.da.animate_close()
            self.drawer_button.setText("Open Drawer")
       
       
# Main entry point        
if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec())
