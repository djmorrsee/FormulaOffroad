class ServerDaemon:
    def __init__(self, cfg):
        self.host = cfg.host
        self.username = cfg.user
        self.password = cfg.password
