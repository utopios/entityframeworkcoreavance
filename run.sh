#!/bin/bash

# URL cible
URL="http://localhost:5199/api/order"

# Nombre total de requêtes
TOTAL_REQUESTS=1000

# Pause après chaque lot de requêtes
PAUSE_INTERVAL=100
PAUSE_DURATION=10

echo "Starting to send $TOTAL_REQUESTS requests to $URL..."

# Boucle pour effectuer les requêtes
for ((i=1; i<=TOTAL_REQUESTS; i++))
do
    # Effectuer une requête HTTP GET
    curl -s -o /dev/null -w "Request $i: HTTP %{http_code}\n" "$URL"

    # Pause toutes les $PAUSE_INTERVAL requêtes
    if (( i % PAUSE_INTERVAL == 0 )); then
        echo "Completed $i requests. Pausing for $PAUSE_DURATION seconds..."
        sleep $PAUSE_DURATION
    fi
done

echo "Finished sending $TOTAL_REQUESTS requests."
