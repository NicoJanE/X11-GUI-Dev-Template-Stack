#!/bin/bash
#
# This file is part of: PHP Development Template Stack 
# Copyright (c) 2024 Nico Jan Eelhart
#
# This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.
#
echo "Starting and running the script for running Apache2 with PHP in the foreground (required)"

/usr/sbin/php-fpm8.2 -F & 
# Use other PHP version FPM
# /usr/sbin/php-fpm5.6 -F & 

# Start Apache2
# WARNING
# Must run in foreground to be primary process in docker!, otherwise communication between PHP-FPM and Apache may break
/usr/sbin/apache2  -f /opt/apache2/conf/httpd.conf -D FOREGROUND
echo "Apache2 PHP startup script is executing"
