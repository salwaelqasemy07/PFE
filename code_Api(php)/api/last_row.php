<?php
$link = mysqli_connect('localhost', 'root', '', 'tracker');
$sql = "SELECT * from adresse ORDER BY id_adress DESC LIMIT 1";
$result = mysqli_query($link, $sql);
if ($result) {
    
    $i=0;
    while ($row=mysqli_fetch_assoc($result)) {
            $response[$i]['id_adress']=$row['id_adress'];
            $response[$i]['adress']=$row['adress'];
            $response[$i]['latitude']=$row['latitude'];
            $response[$i]['longitude']=$row['longitude'];
            $response[$i]['date']=$row['date'];
        $i++;
        # code...
    }
    echo json_encode($response,JSON_PRETTY_PRINT);
}
