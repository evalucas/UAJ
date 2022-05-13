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
    levelsCompleted = 0    #niveles completados por el jugador
    tiempoEnPausaPorNivel = None       #Tiempo que el jugador se pasa en el menu de pausa
    tiempoPorNivel = None
    pauseTime = 0   
    #DATOS DEL FORMULARIO
    age = None
    generalExp = -1
    platformExp = -1
    walljump = -1
    gravity = -1
    level_dificulty = None


    def __init__(self, nLevels) : #,form) seria lo que contenga la informacion de cada formulario, por ahora placeholder
        #inicializamos los vectores, los valores se meten mas tarde
        self.paths = []
        self.nJumps = np.zeros(nLevels)
        self.nDeaths = np.zeros(nLevels)
        self.tiempoPorNivel = np.zeros(nLevels)
        self.tiempoEnPausaPorNivel = np.zeros(nLevels)
        #un vector de vectores de caminos con tamaño nLevels; camino = vector de posiciones
        for i in range(nLevels):
            p = []
            self.paths.append(p)
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
        self.resetPath(actualLevel) #borra el camino guardado hasta el momento, para quedarse con el último de cada nivel

    def pause(self,tiempo):
        if(self.pauseTime != 0):                            #si hay un primer pause                      
            self.tiempoEnPausaPorNivel[self.levelsCompleted] += (tiempo - self.pauseTime) #Anade al tiempo de pausa total el de esta pausa
            self.pauseTime = 0 
        else:                                               #si no, cuenta una pausa
            self.pauseTime += tiempo                        #guarda el timestamp
            self.nPause += 1


    def addToPath(self,pos,actualLevel):
        #añadir una posicion a un camino
        self.paths[actualLevel].append(pos)
    def resetPath(self, actualLevel):
        #borra el camino del nivel en el que toque
        self.paths[actualLevel].clear()
        #print("CLEARING PATH ", actualLevel, "...")
        #print(self.paths[actualLevel])

    def end(self):
        #utilizo el sessionEnd para hacer el print final
        self.informe()

    #Nivel finalizado +1 al contador de niveles superados
    def lvlEnd(self, tiempo):
        self.tiempoPorNivel[self.levelsCompleted] = tiempo
        self.levelsCompleted += 1

    def informe(self): #comento todo el metodo para que no se pete la consola de datos
        print("INFORME JUGADOR ", self.ID)
        print("Jumps: ", self.nJumps)
        print("Deaths: ", self.nDeaths)
        print("Tiempo por nivel: ", self.tiempoPorNivel)
        print("Tiempo en pausa: ", self.tiempoEnPausaPorNivel)
        #for i in range(len(self.paths)):
        #    print("Path ", i, ": ", self.paths[i])
        print("---------------------------------------------")

    def hasLevelBeenCompleted(self, level):
        return level < self.levelsCompleted

    def resetLevelInfo(self, level):
        self.nJumps[level] = 0
        self.nDeaths[level] = 0
        self.tiempoPorNivel[level] = 0
        self.tiempoEnPausaPorNivel[level] = 0
        self.resetPath(level)
