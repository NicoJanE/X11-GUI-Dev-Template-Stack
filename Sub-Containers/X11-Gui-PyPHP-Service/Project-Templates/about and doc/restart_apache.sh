#!/bin/bash
echo "Gracefully restarting Apache..."

# Get the PID of the main Apache process (foreground)
apache_pid=$(ps aux | grep '[a]pache2' | grep -v 'grep' | awk '{print $2}')

# If we find the PID, send the USR1 signal to gracefully restart Apache
if [ -n "$apache_pid" ]; then
  kill -USR1 $apache_pid
  echo "Apache restarted successfully."
else
  echo "Apache is not running."
fi
