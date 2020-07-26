<?php

require 'ConnectionSettings.php';

//variables submited by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// DANGEROUS!
// $sql = "SELECT password, id FROM users where username = '" . $loginUser . "'";
// $result = $conn->query($sql);

// PREPARED STATEMENT!
$sql = "SELECT password, id, level, coins FROM users WHERE username = ?";
$statement = $conn->prepare($sql);
$statement->bind_param("s", $loginUser);  // s: Type string , Use 2 value: "ss".
$statement->execute();
$result = $statement->get_result();


if ($result->num_rows > 0) {
  // output data of each row
  $rows = array();
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass)
    {
        $rows[] = $row;
        echo json_encode($rows);
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