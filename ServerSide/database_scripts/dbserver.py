from flask import Flask, request
from config import *

cfg = Config(file('pysql.conf'))

import create_user
import get_ghosts
import post_ghost

create_user_daemon = create_user.CreateUserDaemon(cfg)
get_ghosts_daemon = get_ghosts.GetGhostsDaemon(cfg)
post_ghost_daemon = post_ghost.PostGhostDaemon(cfg)

app = Flask(__name__);

@app.route('/create_user', methods=['POST'])
def create_user_route():
	data = request.get_data()

@app.route('/get_ghost', methods=['GET'])
def get_ghosts_route():
	data = request.get_data()

@app.route('/post_ghost', methods=['POST'])
def post_ghost_route():
	data = request.get_data()

if __name__ == '__main__':

	app.run(host='0.0.0.0', port=5555);
