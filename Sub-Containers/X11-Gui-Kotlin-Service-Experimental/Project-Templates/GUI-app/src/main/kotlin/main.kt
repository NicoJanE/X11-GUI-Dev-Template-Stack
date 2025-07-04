import androidx.compose.material.*
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.application

fun main() = application {
    Window(onCloseRequest = ::exitApplication, title = "Hello Compose") {
        MaterialTheme {
            Button(onClick = { println("Hello from Compose Desktop!") }) {
                Text("Click me")
            }
        }
    }
}
