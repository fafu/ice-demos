// **********************************************************************
//
// Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
// changed by Fafu for Bluetooth Connection
//
// **********************************************************************

#include <Ice/Ice.h>
#include <IceUtil/UUID.h>
#include <IceBT/ConnectionInfo.h>
#include <Latency.h>

using namespace std;
using namespace Demo;

class LatencyClient : public Ice::Application
{
public:

    virtual int run(int, char*[]);
};


int
main(int argc, char* argv[])
{
#ifdef ICE_STATIC_LIBS
    Ice::registerIceSSL();
#endif
    LatencyClient app;
    return app.main(argc, argv, "config.client");
}

int
LatencyClient::run(int argc, char* argv[])
{
    if(argc > 1)
    {
        cerr << appName() << ": too many arguments" << endl;
        return EXIT_FAILURE;
    }
    
    cout << "Please provide the BT server address purely in this form: 01:AB:23:4A:01:92" << endl;
    String addr;
    cin >> addr;

    PingPrx ping = PingPrx::uncheckedCast(
        communicator()->stringToProxy(
            "peer:bt -a \"" + addr + "\" -u 6a193943-1754-4869-8d0a-ddc5f9a2b294"));
    //old checkedCast(communicator()->propertyToProxy("Ping.Proxy"));
    if(!ping)
    {
        cerr << argv[0] << ": invalid proxy" << endl;
        return EXIT_FAILURE;
    }

    // Initial ping to setup the connection.
    ping->ice_ping();

    IceUtil::Time tm = IceUtil::Time::now(IceUtil::Time::Monotonic);

    const int repetitions = 100000;
    cout << "pinging server " << repetitions << " times (this may take a while)" << endl;
    for(int i = 0; i < repetitions; ++i)
    {
        ping->ice_ping();
    }

    tm = IceUtil::Time::now(IceUtil::Time::Monotonic) - tm;

    cout << "time for " << repetitions << " pings: " << tm * 1000 << "ms" << endl;
    cout << "time per ping: " << tm * 1000 / repetitions << "ms" << endl;

    return EXIT_SUCCESS;
}
