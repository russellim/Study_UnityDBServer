<?php

require 'ConnectionSettings.php';

//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];


$sql = "SELECT password, id FROM users where username = '" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass)
    {
        echo $row["id"];
        //Get user's data here.

        //Get player info.

        //Get Inventory.

        //Modify player data.

        //Update Inventory.
    }
    else
    {
        echo "Wrong Credentials.";
    }
  }
} else {
  echo "Username does not exists.";
}
$conn->close();

?>