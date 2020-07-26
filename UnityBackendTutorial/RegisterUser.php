<?php

require 'ConnectionSettings.php';

//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];


// $sql = "SELECT username FROM users where username = '" . $loginUser . "'";
// $result = $conn->query($sql);

// PREPARED STATEMENT!
$sql = "SELECT username FROM users where username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser);  // s: Type string , Use 2 value: "ss".
$statement->execute();
$result = $statement->get_result();


if ($result->num_rows > 0) {
    // Tell user that name is already taken
    echo "Username is already taken.";
} else {
  echo "Creating...";
  //Insert the user and password into the DB.
  //$sql_insert = "INSERT INTO users (username, password, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "', 1, 0)";
  $sql_insert = "INSERT INTO users (username, password, level, coins) VALUES (?, ?, 1, 0)";
  $statement = $conn->prepare($sql_insert);
  $statement->bind_param("ss", $loginUser, $loginPass);
  $statement->execute();
  $result = $statement->get_result();
  echo "New record created successfully";

  // if ($conn->query($sql_insert) === TRUE) {
  //   echo "New record created successfully";
  // } else {
  //   echo "Error: " . $sql_insert . "<br>" . $conn->error;
  // }
  
}
$conn->close();

?>