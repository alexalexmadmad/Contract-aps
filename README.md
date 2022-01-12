```
<!DOCTYPE html> <!-- just open this file in the browser -->
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>MQTT client example</title>
    <script src="https://unpkg.com/mqtt@3.0.0/dist/mqtt.min.js"></script>
</head>

<body>
    <h1>In-browser MQTT client example</h1>
    <script>
        let temp = "";
        let humid = "";
        function example() {
            var client = mqtt.connect('wss://mqtt.flespi.io', {
                clientId: 'flespi-examples-mqtt-client-browser',
                // see https://flespi.com/kb/tokens-access-keys-to-flespi-platform to read about flespi tokens
                username: 'FlespiToken ' + "30Zeg20AopbivFfYG0I7klFHoZDm8GS1uF3AvR1H60PruSf0IMVTVqq9fTlbn10F",
                protocolVersion: 5,
                clean: true,
            });

            client.on('connect', () => {
                console.log('connected, subscribing');
                client.subscribe('easv/weather/humid', { qos: 0 }, (err) => {
                    if (err) {
                        console.log('failed to subscribe to topic "test":', err);
                        return;
                    }

                });
                client.subscribe('easv/weather/tempC', { qos: 0 }, (err) => {
                    if (err) {
                        console.log('failed to subscribe to topic "test":', err);
                        return;
                    }
                });


            });

            client.on('message', (topic, msg) => {
                if (topic == "easv/weather/humid") {
                    console.log('received message in topic "' + topic + '": "' + msg.toString('utf8') + '"');
                    humid = msg.toString('utf8');
                    console.log("humid is : " + humid);
                }
                if (topic == "easv/weather/tempC") {
                    console.log('received message in topic "' + topic + '": "' + msg.toString('utf8') + '"');
                    temp = msg.toString('utf8');
                    console.log("temp is : " + temp);
                }

            });

            client.on('error', (err) => {
                client.end(true) // force disconnect
            });
        }
        window.onload = function () {
            example();
        };

    </script>
</body>

</html>
```
