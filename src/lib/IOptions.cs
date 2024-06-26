﻿namespace lib;

public interface IOptions
{
    
    // attributes
    bool Comment { get; }
    bool IsExactWord { get; }
    string Query { get; }
    string Filter { get; }
    int Limit { get; }
    string User { get; }

    // behavior
    string GetFilterValue(string key);
    
}
