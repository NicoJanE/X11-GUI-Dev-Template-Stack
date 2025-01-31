<?php
require_once  '../config/config.php';
require_once  '../src/controller.php';

use App\Controller;

$controller = new Controller();
$controller->home();
?>
