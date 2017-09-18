<?php
    $apiKey = 'YOUR_API_KEY';
    $apiSecret = 'YOUR_API_SECRET';
    $apiUrl = 'https://api.changelly.com';

    $message = json_encode(
        array('jsonrpc' => '2.0', 'id' => 1, 'method' => 'getCurrencies', 'params' => array())
    );
    $sign = hash_hmac('sha512', $message, $apiSecret);
    $requestHeaders = [
        'api-key:' . $apiKey,
        'sign:' . $sign,
        'Content-type: application/json'
    ];

    $ch = curl_init($apiUrl);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $message);
    curl_setopt($ch, CURLOPT_HTTPHEADER, $requestHeaders);
    
    $response = curl_exec($ch);
    curl_close($ch);

    var_dump($response);
?>
