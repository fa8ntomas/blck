from uuid import uuid4
from yattag import Doc
from yattag import indent
import sys

doc, tag, text = Doc().tagtext()

class XexSegment:
    start:int=-1
    end=-1
    blob_data=bytearray()
    def __init__(self, start, end, data):
        self.blob_data=data
        self.start=start
        self.end=end

segments=[]

def readSegment(f):
    word=f.read(2)
    if len(word)==0:
        # EOF
        return False
    start = int.from_bytes(word, byteorder='little')
    if start == 0xFFFF:
        # $FFFF - Indicates a binary load file. Mandatory for first segment, optional for any other segment
        start = int.from_bytes(f.read(2), byteorder='little')
    end = int.from_bytes(f.read(2), byteorder='little')
    print("load ",hex(start))
    segments.append(XexSegment(start,end,f.read(end-start+1)))
    return True

def buildMem():
    bank = bytearray([0x0]*0xFFFF)
    for segment in segments:
        bank[segment.start:segment.end+1] = segment.blob_data
    return bank

def word(l,h):
    return h*256+l

def exportFont(font,mem):
    filename=f'font{font.index}.fnt'
    with open(filename, "wb") as fontFile:
        fontFile.write(mem[font.adr:font.adr+1024])
        fontFile.close()
    return filename

def exportRle(room,mem):
    filename=f'Map{room.index}.rle'
    with open(filename, "wb") as mapFile:
        mapFile.write(mem[room.adr:room.adr+room.length])
        mapFile.close()
    return filename

def generateName(roomIndex):
    return "Map "+str(roomIndex)

with open("c:/temp/BruceLeeXEX.xex", "rb") as f:
    while readSegment(f):
        pass    
    f.close()

a8mem=buildMem()

def getFontAdrList(mem):
    fontAdr=set() 
    for roomIndex in range(0,21):
        fontAdr.add(word(0,a8mem[0x3F17+roomIndex]))
    return list(fontAdr)

class blfont:
    adr=0
    index=-1
    uuid=''
    def __init__(self, adr, index, uuid):
        self.adr=adr
        self.index=index
        self.uuid=uuid

fontList=getFontAdrList(a8mem)
fonts=[]
for font in fontList:
    fonts.append(blfont(font,fontList.index(font), str(uuid4())))

def getFont(fontAdr):
    for font in fonts:
        if font.adr==fontAdr:
            return font
    return None

class Dli:
    lines=[]
    adr=0
    def __init__(self, lines, adr):
        self.lines=lines
        self.adr=adr

dlis=[]
for roomIndex in range(0,20):
    lines=[]
    idli=word(a8mem[0x0F38+roomIndex],a8mem[0x0F4C+roomIndex])
    length = 0
    while (idli):
        if (idli & 1):
            lines.append(length)
        idli >>= 1
        length += 1
    dlis.append(Dli(lines,word(a8mem[0x3E8B+roomIndex*2],a8mem[0x3E8C+roomIndex*2])))
  
class Room:
    index=-1
    adr=0
    length=-1
    fontadr=0
    uid=''
    def __init__(self, index, adr, length,fontadr):
        self.index=index
        self.adr=adr
        self.length=length
        self.fontadr=fontadr
        self.uid=str(uuid4())



rooms=[]
for roomIndex in range(0,20):
    adr=word(a8mem[0x3F2B+roomIndex], a8mem[0x3F40+roomIndex])
    length=word(a8mem[0x3F2B+roomIndex+1], a8mem[0x3F40+roomIndex+1])-adr
    fontadr=word(0,a8mem[0x3F17+roomIndex])
    rooms.append(Room(roomIndex,adr,length,fontadr))

def getRoomUUIDByIndex(index):
    for room in rooms:
        if (room.index==index):
            return room.uid
    return ''

MapExits1=0x3F40+21
MapExits2=MapExits1+20
MapExits3=MapExits2+20
MapExits4=MapExits3+20
MapBruceStartX1=MapExits4+20
MapBruceStartY1=MapBruceStartX1+20
MapBruceStartX2=MapBruceStartY1+20
MapBruceStartY2=MapBruceStartX2+20
MapExit1X=MapBruceStartY2+20
MapExit1Y=MapExit1X+20
MapExit2X=MapExit1Y+20
MapExit2Y=MapExit2X+20
MapExit3X=MapExit2Y+20
MapExit3Y=MapExit3X+20
MapExit4X=MapExit3Y+20
MapExit4Y=MapExit4X+20
MapFoeFlags=MapExit4Y+20

print(a8mem[MapExits1],a8mem[MapExits2],a8mem[MapExits3],a8mem[MapExits4],a8mem[MapBruceStartX1],a8mem[MapBruceStartY1])

doc, tag, text = Doc().tagtext()

doc.asis('<?xml version="1.0" encoding="utf-8"?>')
with tag('bleditor'):
    for font in fonts:
        doc.stag('font', uid=font.uuid, path=exportFont(font, a8mem))
    for room in rooms:
        with tag('map', name=generateName(room.index),font=getFont(room.fontadr).uuid, path=exportRle(room,a8mem), uid=room.uid):
            with tag('dlis'):
                dli=dlis[room.index]
                coloradr=dli.adr
                doc.stag('dli', order='COLBK / COLPF0 / COLPF1 / COLPF2', row=str(0), colbk=str(a8mem[coloradr]), colpf3=str(a8mem[coloradr+1]), colpf2=str(a8mem[coloradr+2]), colpf1=str(a8mem[coloradr+3]), colpf0=str(a8mem[coloradr+4]))
                for line in dli.lines:
                    coloradr+=5
                    doc.stag('dli', order='COLBK / COLPF0 / COLPF1 / COLPF2', row=str(line+1), colbk=str(a8mem[coloradr]), colpf3=str(a8mem[coloradr+1]), colpf2=str(a8mem[coloradr+2]), colpf1=str(a8mem[coloradr+3]), colpf0=str(a8mem[coloradr+4]))
            with tag('foe'):
                text("true" if a8mem[MapFoeFlags+room.index]==0 else "false")
            with tag('exit1'):
                with tag('map'):
                    text(getRoomUUIDByIndex(a8mem[MapExits1+room.index]))   
                with tag('x'):
                    text(a8mem[MapExit1X+room.index])   
                with tag('y'):
                    text(a8mem[MapExit1Y+room.index])   
            with tag('exit2'):
                with tag('map'):
                    text(getRoomUUIDByIndex(a8mem[MapExits2+room.index]))   
                with tag('x'):
                    text(a8mem[MapExit2X+room.index])   
                with tag('y'):
                    text(a8mem[MapExit2Y+room.index])   
            with tag('exit3'):
                with tag('map'):
                    text(getRoomUUIDByIndex(a8mem[MapExits3+room.index]))   
                with tag('x'):
                    text(a8mem[MapExit3X+room.index])   
                with tag('y'):
                    text(a8mem[MapExit3Y+room.index])   
            with tag('exit4'):
                with tag('map'):
                    text(getRoomUUIDByIndex(a8mem[MapExits4+room.index]))   
                with tag('x'):
                    text(a8mem[MapExit4X+room.index])   
                with tag('y'):
                    text(a8mem[MapExit4Y+room.index])   
            with tag('Colpf0Dectection'):
                with tag('Type'):
                    text('Always')
            with tag('Colpf1Dectection'):
                with tag('Type'):
                    text('Always')
            with tag('Colpf2Dectection'):
                with tag('Type'):
                    text('Always')

result = indent(
    doc.getvalue(),
    indentation = '    ',
    newline = '\n',
    indent_text = True
)

with open("brucelee.xml", "w") as f:
    f.write(result)
    f.close()

