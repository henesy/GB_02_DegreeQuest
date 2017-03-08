#!/bin/bash
# This script updates the site branch of DegreeQuest with the latest website files from http://proj-309-gb-2.cs.iastate.edu
# The original site runs on RHEL with Apache 2.4.x and Werc 1.4.0 (as per http://werc.cat-v.org)

cp -R /var/www/html/* ./
git add *
#time=$(date +%s)
time=$(`date --rfc-3339=seconds`)
msg="Server updating site archive on: "
commit=$msg$time
git commit -m "$commit"
git push

