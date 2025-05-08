<?php
header("Content-Type: application/json");

$method = $_SERVER['REQUEST_METHOD'];
// The following 2 lines where not allowe in the launch.json file, so putenv is our alternative
// Make sure to use differsnt sessions ID's for each project,,otherwise breakpoints and debbuging may fail after switching between projects
//"XDEBUG_SESSION": "project1, PHP(A): Xdebug  AFX"
putenv('XDEBUG_SESSION=project_2rest');  

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
