#!/bin/bash

FRONTEND_CERTS="/frontend/certs"
CERT_PATH="${FRONTEND_CERTS}/portfoliosis.crt"
NGINX_SSL_PORT="${NGINX_SSL_PORT:-8083}"

response_code=$(curl -k --cacert "${CERT_PATH}" -s -o /dev/null -w "%{http_code}" "https://localhost:${NGINX_SSL_PORT}")
echo "HTTPS Response Code: ${response_code}"

if [ "${response_code}" -eq 200 ]; then
    echo "Integration test passed"
else
    echo "Integration test failed"
    echo "Frontend responded with code ${response_code}"
    exit 1
fi