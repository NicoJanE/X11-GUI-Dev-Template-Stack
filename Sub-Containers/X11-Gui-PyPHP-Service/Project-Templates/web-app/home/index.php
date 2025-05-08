<?php
require_once  '../config/config.php';
require_once  '../src/controller.php';

use App\Controller;

// The following 2 lines where not allowe in the launch.json file, so putenv is our alternative
// Make sure to use differsnt sessions ID's for each project,,otherwise breakpoints and debbuging may fail after switching between projects
//"XDEBUG_SESSION": "project1, PHP(A): Xdebug  AFX"
putenv('XDEBUG_SESSION=project1_app'); 

$controller = new Controller();
$controller->home();
?>
