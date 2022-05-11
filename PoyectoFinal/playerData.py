from ast import If
import json
import os
import glob
import numpy as np

class Player:
    ID = -1 #id del usuario
    nJumps = None #saltos por cada nivel
    nDeaths = None #muertes por cada nivel
    paths = None #caminos recorridos por cada nivel
    nPause = 0 #numero de veces que ha pausado

    #DATOS DEL FORMULARIO
    age = None
    generalExp = -1
    platformExp = -1
    walljump = -1
    gravity = -1
    level_dificulty = None


    def __init__(self, nLevels) : #,form) seria lo que contenga la informacion de cada formulario, por ahora placeholder
        #inicializamos los vectores, los valores se meten mas tarde
        self.nJumps = np.zeros(nLevels)
        self.nDeaths = np.zeros(nLevels)
        #self.paths = un vector de vectores de caminos con tamaño nLevels; camino = vector de posiciones
        #Guardar datos del formulario, por ahora esta aqui pero puede ir en otro lado
        #self.age = 
        #self.generalExp = 
        #self.platformExp = 
        #self.walljump =
        #self.gravity = 
        #self.level_dificulty = array con las cuatro opciones, ya sea un string o la parseamos 
    
    #metodos de eventos
    def setID(self, ID):
        self.ID = ID
    def jump(self,actualLevel):
        self.nJumps[actualLevel]+=1
    def death(self,actualLevel):
        self.nDeaths[actualLevel]+=1
        #self.resetPath(actualLevel) #borra el camino guardado hasta el momento, para quedarse con el último de cada nivel
    def pause(self):
        self.nPause+=1
    #def addToPath(self,pos,actualLevel):
        #añadir una posicion a un camino
    #def resetPath(self, actualLevel):
        #borra el camino del nivel en el que toque

