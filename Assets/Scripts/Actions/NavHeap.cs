using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// heap of NavNode objects, can be min by totalMoveCost (ai = false), or max by f (ai = true)
public class NavHeap {

	private bool ai; // boolean to determine if it should follow min general or max behavior ai
	private int size; // current size of the heap
	private NavNode[] array; // the heap data structure

	public NavHeap (int maxSize, bool isAI) {
		array = new NavNode[maxSize + 1];
		size = 0;
		ai = isAI;
	}

	public void push(NavNode myNode) {
		size ++;
		array[size] = myNode;

		// select min movement or max f sorting
		if (ai)
			aiSiftUp(size);
		else
			siftUp(size);
	}

	public NavNode pop() {
		// return null if no elements in the heap
		if (size < 1)
			return null;
		
		// set aside root node, and swap in terminal node
		array[0] = array[1];
		array[1] = array[size];
		size --;

		// select min movement or max f sorting
		if (ai)
			aiHeapify(1);
		else
			heapify(1);

		// return the root element
		return array[0];
	}

	// sifts up from the specified index (recursive), min swapping for movement
	private void siftUp(int index) {
		// return if we're at the root
		if (index <= 1)
			return;
		
		// get tree parent index
		int parentIndex = index / 2;

		// min swapping, swap if index < parent by move cost
		if(array[index].totalMoveCost < array[parentIndex].totalMoveCost) {
			NavNode temp = array[index];
			array[index] = array[parentIndex];
			array[parentIndex] = temp;

			// and then recursive call to parent index
			siftUp(parentIndex);
		}
	}

	// sifts up from specified index (recursive), max swapping for ai's f
	private void aiSiftUp(int index) {
		// return if we're at the root
		if (index <= 1)
			return;
		
		// get tree parent index
		int parentIndex = index / 2;

		// max swapping, swap if index > parent by f
		if(array[index].f > array[parentIndex].f) {
			NavNode temp = array[index];
			array[index] = array[parentIndex];
			array[parentIndex] = temp;

			// and then recursive call to parent index
			aiSiftUp(parentIndex);
		}
	}

	// heapify's the whole array, from the top, recursive, min for movement
	private void heapify(int parent) {
		// look for left child's index, return if no children
		int leftChild = parent * 2;
		if(leftChild > size)
			return;

		// set prefered child, and get right child's index
		int rightChild = leftChild +1;
		int preferedChild = leftChild;

		// compare for prefered child if there are two children
		if (!(rightChild > size)) {
			if (array[rightChild].totalMoveCost < array[rightChild].totalMoveCost)
				preferedChild = rightChild;
		}

		// if the preffered child's movement is < parent, swap
		if(array[preferedChild].totalMoveCost < array[parent].totalMoveCost) {
			NavNode temp = array[preferedChild];
			array[preferedChild] = array[parent];
			array[parent] = temp;
		
			// continue to heapify
			heapify(preferedChild);
		}
	}

	// heapify's the whole array, from the top, recursive, max for ai's f
	private void aiHeapify(int parent) {
		// look for left child's index, return if no children
		int leftChild = parent * 2;
		if(leftChild > size)
			return;

		// set prefered child, and get right child's index
		int rightChild = leftChild +1;
		int preferedChild = leftChild;

		// compare for prefered child if there are two children
		if (!(rightChild > size)) {
			if (array[rightChild].f > array[rightChild].f)
				preferedChild = rightChild;
		}

		// if the preffered child's f > parent, swap
		if(array[preferedChild].f > array[parent].f) {
			NavNode temp = array[preferedChild];
			array[preferedChild] = array[parent];
			array[parent] = temp;
		
			// continue to heapify
			aiHeapify(preferedChild);
		}
	}
}
