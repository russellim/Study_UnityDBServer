<?php

require 'ConnectionSettings.php';

//User submited variables.
$userID = $_POST["userid"];


$sql = "SELECT id, itemid FROM usersitems WHERE userID = '" . $userID . "'";
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