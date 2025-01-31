<?php
header("Content-Type: application/json");

$method = $_SERVER['REQUEST_METHOD'];

switch($method) {
    case 'GET':
        if (isset($_GET['name'])) {
            $name = $_GET['name'];
            echo json_encode(["message" => "Hello, $name!"]);
        } else {
            echo json_encode(["message" => "Hello, world!"]);
        }
        break;
    
    case 'POST':
        $input = json_decode(file_get_contents('php://input'), true);
        if (isset($input['name'])) {
            $name = $input['name'];
            echo json_encode(["message" => "Hello, $name! (from POST)"]);
        } else {
            echo json_encode(["error" => "Name not provided"]);
        }
        break;

    default:
        echo json_encode(["error" => "Method not allowed"]);
        break;
}
?>
