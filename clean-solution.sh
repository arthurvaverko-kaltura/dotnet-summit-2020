#!/bin/bash

echo "Removing bin folders..."
find . -iname "bin" | xargs rm -rf

echo "Removing obj folders..."
find . -iname "obj" | xargs rm -rf

echo "Removing .vs folder to clean visual studio cache..."
find . -iname ".vs" | xargs rm -rf
