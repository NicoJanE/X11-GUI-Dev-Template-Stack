<?php

# BE-AWARE:  8071 is mapped port for Windows Host!
#            internal docker uses 80!   
#   So the direct sample call:
#   http://localhost:8071/web-rest/wr-api.php?name=John # WORKS FROM HOST
#   http://localhost:80/web-rest/wr-api.php?name=John # WORKS FROM CONTAINER
#
$api_url = 'http://localhost:80/web-rest/wr-api.php';
#
#Alternative use internal (not preffered)
#$api_url = 'http://host.docker.internal:8071/web-rest/wr-api.php';
# 

// Test GET request
$get_response = file_get_contents($api_url . '?name=John');
if ($get_response === false) {
    echo "GET request failed. Check the URL or server.<br>";
} else {
    echo "GET request response: " . $get_response . "<br>";
}

// Test POST request
$post_data = json_encode(['name' => 'John']);
$opts = [
    'http' => [
        'method'  => 'POST',
        'header'  => "Content-Type: application/json\r\n",
        'content' => $post_data,
    ],
];
$context  = stream_context_create($opts);
$post_response = file_get_contents($api_url, false, $context);
if ($post_response === false) {
    echo "POST request failed. Check the URL or server.<br>";
} else {
    echo "POST request response: " . $post_response . "<br>";
}
?>