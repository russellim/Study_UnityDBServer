<?php

require 'ConnectionSettings.php';

$itemID = $_POST["itemid"];

$path = "http://localhost/unityBackendTutorial/ItemIcons/" . $itemID . ".png";


// Get the image and convert into string.
$image = file_get_contents($path);

echo $image;

$conn->close();

?>