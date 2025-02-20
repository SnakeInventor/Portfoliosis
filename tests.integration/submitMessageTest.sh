#!/bin/bash
BACKEND_SSL_PORT="${BACKEND_SSL_PORT:-8081}"
BACKEND_CERTS="/backend/certs"
CERT_PATH="${BACKEND_CERTS}/portfoliosisbackend.pem"

POSTGRES_DB_DBNAME="${POSTGRES_DB_DBNAME:-portfoliosis_test}"
POSTGRES_DB_USER="${POSTGRES_DB_USER:-testuser}"

test_name="Name"
test_email="local.name@sample.email.com"
test_message="This\nis\na\nmessage"

# clean all rows in the database
docker exec -i portfoliosis-database-1 psql -U "${POSTGRES_DB_USER}" -d "${POSTGRES_DB_DBNAME}" -c "TRUNCATE TABLE \"Message\"" --csv -t

# send post request to backend
request_json="{\"name\":\"${test_name}\",\"email\":\"${test_email}\",\"text\":\"${test_message}\"}"

response_code=$(curl -k -o /dev/null -w "%{http_code}" --cacert "${CERT_PATH}" -X POST -H "Content-Type: application/json" --data-raw "${request_json}" "https://localhost:${BACKEND_SSL_PORT}/api/messages")
if [ "${response_code}" -eq 200 ]; then
  echo "POST request to api successfull"
else
  echo "Integration test failed"
  echo "Backend api responded with code ${response_code}"
  exit 1
fi

# wait for backend to process request
echo "Waiting 5 seconds for backend to process the message"
sleep 5s

# query database for submitted message
database_query="SELECT \"Name\", \"Email\", \"Text\" FROM \"Message\"";

echo "Quering database for the message"
query_result=$(docker exec -i portfoliosis-database-1 psql -U "${POSTGRES_DB_USER}" -d "${POSTGRES_DB_DBNAME}" -c "${database_query}" --csv -t)

printed_test_message=$(printf "%s${test_message}") # to convert "\n" to actual newline characters
expected_result="${test_name},${test_email},\"${printed_test_message}\""

# clean all leftover rows in the database
echo "Cleaning up test data"
docker exec -i portfoliosis-database-1 psql -U "${POSTGRES_DB_USER}" -d "${POSTGRES_DB_DBNAME}" -c "TRUNCATE TABLE \"Message\"" --csv -t

if [ "${query_result}" = "${expected_result}" ]; then
    echo "Integration test passed"
else
    echo "Integration test failed"
    echo "Expected query result: ${expected_result}"
    echo "Actual query result:   ${query_result}"
    exit 1
fi