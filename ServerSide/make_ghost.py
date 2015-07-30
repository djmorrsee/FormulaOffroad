import random
import os

from struct import *

VALUES_PER_TICK = 8

def MakeFakeFloats(ticks, num_vals):
    floats = []
    for i in range(0, ticks):
        for j in range(0, num_vals):
            value = random.random() * 100
            floats.append(value)

    return floats

def MakeBytes (float_array):
    bytes = []

    for val in float_array:
        bytes.append(bytearray(pack('f', val))) # 4 byte precision, consider doubles?

    return bytes

def main():

    # Simulated values
    duration = 120 # Length in seconds
    fps = 15 # Ticks per second
    ticks = fps * duration
    floats = MakeFakeFloats(ticks, VALUES_PER_TICK)

    # Write String To File (later, to SQL)
    bytefile = open('ghostbyte.txt', 'w')

    # Detect Length in Ticks
    ticks = len(floats) / VALUES_PER_TICK

    # Float to Byte Array
    bytes = MakeBytes(floats)

    # Encode number of ticks in bytestring
    bytefile.write(bytearray(pack('I', ticks)))

    # Write Bytes
    for i in bytes:
        bytefile.write(i)

    bytefile.close()

if __name__ == '__main__':
    main()
