// **********************************************************************
//
// Copyright (c) 2003-2015 ZeroC, Inc. All rights reserved.
//
// **********************************************************************

#pragma once

module Filesystem
{
    exception GenericError
    {
        string reason;
    };
    exception PermissionDenied extends GenericError {};
    exception NameInUse extends GenericError {};
    exception NoSuchName extends GenericError {};

    interface Node
    {
        idempotent string name();
        void destroy() throws PermissionDenied;
    };

    sequence<string> Lines;

    interface File extends Node
    {
        idempotent Lines read();
        idempotent void write(Lines text) throws GenericError;
    };

    enum NodeType { DirType, FileType };

    struct NodeDesc
    {
        string name;
        NodeType type;
        Node* proxy;
    };

    sequence<NodeDesc> NodeDescSeq;

    interface Directory extends Node
    {
        idempotent NodeDescSeq list();
        idempotent NodeDesc find(string name) throws NoSuchName;
        File* createFile(string name) throws NameInUse;
        Directory* createDirectory(string name) throws NameInUse;
    };
};
