# ToDo List

### Notes

    Date = (YYYY-MM-DD)

    Obsidian

## Expandable heaps - (2022-05-07)

### Notes

    Right now the min heaps require a fixed size that is unchangable. It would be beneficial to create an expandable heap class or alter current heaps to expand. In addition, if the size is fixed it might be worth replacing values if the new addition is better than the old one. IE) Adding 3 to a min heap with a value of 5.

### Changes

1. Add/Alter heaps to support this functionality

### Branch

    N/A

### Status

    Not started

## Linked List Nodes -  (2022-05-07)

### Notes

    Right now a node receives a refernce to both the previous and next node in the list, but it itself does not actually make sure the references in the other nodes are correct. By making this change in the node class, it will make the code look cleaner and guarantee accuracy in the nodes

### Changes

1. Add/Alter heaps to support this functionality

### Branch

    N/A

### Status

    In progress. Added to ordered list, but not in DoublyLinkedList or linked list classes

## Add IsEmpty to IListExtened - (2022-05-21)

### Notes

    An is empty function would be nice to have with all collections that Ive built. 

### Changes

1. Add IsEmpty to IListExtended interface and implement in all child classes

### Branch

    N/A

### Status

    Not started
