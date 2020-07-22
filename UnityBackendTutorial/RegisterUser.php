<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackendtutorial";

//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM users where username = '" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Tell user that name is already taken
    echo "Username is already taken.";
} else {
  echo "Creating...";
  //Insert the user and password into the DB.
  $sql_insert = "INSERT INTO users (username, password, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "', 1, 0)";
  if ($conn->query($sql_insert) === TRUE) {
    echo "New record created successfully";
  } else {
    echo "Error: " . $sql_insert . "<br>" . $conn->error;
  }
  
}
$conn->close();

?>