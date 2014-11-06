LxRedux
=======

Lx work revisited. Hopefully a lightweight (re)implementation of the "lexicographical" universe consisting of alphabets, sequences of alphabets, and elements (or points, or words) represented as text values, numeric values, or sequences of indices (defined on sequences of alphabets).

No optimization has been done. Also, all operations on points are within the same space, no between-space operations (compatibility of abc sequences and so on).

Also, more fundamentally, errors are thrown as exceptions. Using something like an Option might be preferable but it could also scare people away which is not the goal here.
