<?php

require 'ConnectionSettings.php';

//User submited variables.
$ID = $_POST["id"];
$itemID = $_POST["itemid"];
$userID = $_POST["userid"];


$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Store item price.
    $itemPrice = $result->fetch_assoc()["price"];

    //Second Sql (delete item)
    $sql2 = "DELETE FROM usersitems WHERE id = '" . $ID . "'";
    $result2 = $conn->query($sql2);
    if($result2)
    {
        $sql3 = "UPDATE users SET coins = coins + '" . $itemPrice . "' WHERE id = '" . $userID . "'"; 
        $conn->query($sql3);

        $sql4 = "SELECT coins FROM users WHERE id = '" . $userID . "'";
        $result4 = $conn->query($sql4);
        $info=mysqli_fetch_array($result4);
        echo $info['coins'];
    }
    else
    {
        echo "could not delete item";
    }
} else {
  echo "0";
}
$conn->close();

?>