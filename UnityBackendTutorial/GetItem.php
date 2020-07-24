<?php

require 'ConnectionSettings.php';

//variables submited by user
$itemID = $_POST["itemid"];


$sql = "SELECT name, description, price FROM items where id = '" . $itemID . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    $rows = array();
    while($row = $result->fetch_assoc()) {
        $rows[] = $row;
    }
    // atfer the whole array is created.
    echo json_encode($rows);
} else {
  echo "0";
}
$conn->close();

?>