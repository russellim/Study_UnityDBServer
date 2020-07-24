<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackendtutorial";

//User submited variables.
$userID = $_POST["userid"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT itemid FROM usersitems WHERE userID = '" . $userID . "'";
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