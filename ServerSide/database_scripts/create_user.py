from server_daemon import ServerDaemon

class CreateUserDaemon(ServerDaemon):
    def CreateUser(self, data):
        print data
