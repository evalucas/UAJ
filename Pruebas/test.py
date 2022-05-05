from ast import If
import json
import os
import glob
import numpy as np

def Process(f):
    
    t_start = None


def ProcessPlayerPosition(f):
    for i in range(0, 5, 1):
        f.readline[0]


def Clean(f):
    session_start = []
    for i in range(0, f.readlines, 1):
        line = f.readline[i]
        
#List of files in folder
for filename in glob.glob('*.json'):
    with open(os.path.join(os.getcwd(), filename), 'r') as f: # open files in readonly mode
        print("Processing file " + f.name + "\n")
        Process(f)
