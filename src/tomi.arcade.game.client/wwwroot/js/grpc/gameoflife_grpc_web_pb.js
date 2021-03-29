/**
 * @fileoverview gRPC-Web generated client stub for gameoflife
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!


/* eslint-disable */
// @ts-nocheck



const grpc = {};
grpc.web = require('grpc-web');

const proto = {};
proto.gameoflife = require('./gameoflife_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.gameoflife.GameOfLifeServiceClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?Object} options
 * @constructor
 * @struct
 * @final
 */
proto.gameoflife.GameOfLifeServicePromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options['format'] = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.gameoflife.GameStateRequest,
 *   !proto.gameoflife.GameStateResponse>}
 */
const methodDescriptor_GameOfLifeService_GetState = new grpc.web.MethodDescriptor(
  '/gameoflife.GameOfLifeService/GetState',
  grpc.web.MethodType.SERVER_STREAMING,
  proto.gameoflife.GameStateRequest,
  proto.gameoflife.GameStateResponse,
  /**
   * @param {!proto.gameoflife.GameStateRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.gameoflife.GameStateResponse.deserializeBinary
);


/**
 * @const
 * @type {!grpc.web.AbstractClientBase.MethodInfo<
 *   !proto.gameoflife.GameStateRequest,
 *   !proto.gameoflife.GameStateResponse>}
 */
const methodInfo_GameOfLifeService_GetState = new grpc.web.AbstractClientBase.MethodInfo(
  proto.gameoflife.GameStateResponse,
  /**
   * @param {!proto.gameoflife.GameStateRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.gameoflife.GameStateResponse.deserializeBinary
);


/**
 * @param {!proto.gameoflife.GameStateRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.gameoflife.GameStateResponse>}
 *     The XHR Node Readable Stream
 */
proto.gameoflife.GameOfLifeServiceClient.prototype.getState =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/gameoflife.GameOfLifeService/GetState',
      request,
      metadata || {},
      methodDescriptor_GameOfLifeService_GetState);
};


/**
 * @param {!proto.gameoflife.GameStateRequest} request The request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @return {!grpc.web.ClientReadableStream<!proto.gameoflife.GameStateResponse>}
 *     The XHR Node Readable Stream
 */
proto.gameoflife.GameOfLifeServicePromiseClient.prototype.getState =
    function(request, metadata) {
  return this.client_.serverStreaming(this.hostname_ +
      '/gameoflife.GameOfLifeService/GetState',
      request,
      metadata || {},
      methodDescriptor_GameOfLifeService_GetState);
};


module.exports = proto.gameoflife;

