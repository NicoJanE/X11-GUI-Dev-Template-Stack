import sys
from PySide6.QtCore import QPropertyAnimation, QRect
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QWidget


class MainWindow(QMainWindow):
    def __init__(self):
        super().__init__()

        self.setWindowTitle("Animated Drawer Example")
        self.setGeometry(100, 100, 400, 300)

        # Create a main button to trigger the drawer animation
        self.drawer_button = QPushButton("Open Drawer", self)
        self.drawer_button.setGeometry(150, 20, 100, 40)
        self.drawer_button.clicked.connect(self.toggle_drawer)

        # Create the drawer widget (hidden initially)
        self.drawer = QWidget(self)
        self.drawer.setStyleSheet("background-color: lightblue;")
        self.drawer.setGeometry(0, 80, 0, 200)  # Start with zero width
        self.drawer.is_open = False

        # Animation setup
        self.drawer_animation = QPropertyAnimation(self.drawer, b"geometry")
        self.drawer_animation.setDuration(500)  # 500ms duration

    def toggle_drawer(self):
        """Animate the drawer to open or close."""
        if not self.drawer.is_open:
            # Animate drawer to open
            self.drawer_animation.setStartValue(QRect(0, 80, 0, 200))
            self.drawer_animation.setEndValue(QRect(0, 80, 200, 200))
            self.drawer_button.setText("Close Drawer")
        else:
            # Animate drawer to close
            self.drawer_animation.setStartValue(QRect(0, 80, 200, 200))
            self.drawer_animation.setEndValue(QRect(0, 80, 0, 200))
            self.drawer_button.setText("Open Drawer")

        self.drawer_animation.start()
        self.drawer.is_open = not self.drawer.is_open


if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec())
