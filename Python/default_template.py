import sys

def process():
    print("Hello, World!")

def main(args):
    try:
        process() # deal with arguments here
    except Exception as e:
        print(str(e))

if __name__ == "__main__":
    main(sys.argv[1:]) # 1: removes the filename from argv
