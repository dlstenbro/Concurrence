import { Stream } from './streams.model';

export type APIResponse = {
  streams : Array<Stream>,
  length: number,
  page ?: string
};
