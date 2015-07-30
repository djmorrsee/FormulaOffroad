import random
import os

from struct import *

VALUES_PER_TICK = 8

def main():
    # Read from file (Later, from SQL)
    f = open('ghostbyte.txt', 'r')

    # Read number of ticks
    tick_bytes = f.read(4) # lol tick bytes
    ticks = unpack('I', tick_bytes)[0]

    # Read rest of bytes
    byte_string = f.read()

    # Decode into float array
    floats = unpack(VALUES_PER_TICK*ticks*'f', byte_string) # 4 byte precision, consider doubles?

    f.close()


if __name__ == '__main__':
    main()
