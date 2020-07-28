<?php

require 'ConnectionSettings.php';

$sql = "SELECT username, level FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "username: " . $row["username"]. 
         " - level: " . $row["level"]. 
         "<br>";
  }
} else {
  echo "0 results";
}
$conn->close();

?>